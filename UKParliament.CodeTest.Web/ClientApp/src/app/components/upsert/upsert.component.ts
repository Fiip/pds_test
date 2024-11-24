import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PersonService } from '../../services/person.service';
import { DepartmentService } from '../../services/department.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PersonViewModel } from '../../models/person-view-model';
import { Observable } from 'rxjs';
import { DepartmentViewModel } from '../../models/department-view-model';
import { PersonUpsertViewModel } from '../../models/person-upsert-view-model';

@Component({
  selector: 'app-upsert-component',
  templateUrl: './upsert.component.html',
  styleUrls: ['./upsert.component.scss']
})
export class UpsertComponent implements OnInit {
  @Input() person: PersonViewModel | null = null;
  @Output() update = new EventEmitter<PersonViewModel>();
  @Output() create = new EventEmitter<PersonViewModel>();
  @Output() cancel = new EventEmitter<void>();

  editForm: FormGroup;
  departments: Observable<DepartmentViewModel[]> | null = null;
  constructor(private fb: FormBuilder, private personService: PersonService, private departmentService: DepartmentService) {
    this.editForm = this.createForm();
  }

  ngOnInit() {
    if (this.person) {
      this.editForm.patchValue(this.person);
    }

    this.departments = this.departmentService.list();
  }

  private createForm(): FormGroup {
    return this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      birthDate: ['', Validators.required],
      departmentId: ['', Validators.required],
    });
  }

  private parseDate(dateString: string): Date {
    return new Date(dateString);
  }

  onDateChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.value) {
      const date = this.parseDate(input.value);
      if (isNaN(date.getTime())) {
        this.editForm.get('birthDate')?.setErrors({ 'invalidDate': true });
      }
    }
  }

  onSubmit() {
    if (this.editForm.valid && this.person) {
      const updatedPerson = { ...this.person, ...this.editForm.value };
      if (this.person.id === 0) {
        this.personService.create(updatedPerson).
          subscribe({
            next: (response) => {
              console.log('Person created:', response);
              this.create.emit(updatedPerson);
            },
            error: (error) => {
              console.error('Error creating person:', error);
            }
          });
      }
      else {
        this.personService.update(this.person.id, updatedPerson).
          subscribe({
            next: (response) => {
              console.log('Person updating:', response);
              this.update.emit(updatedPerson);
            },
            error: (error) => {
              console.error('Error updating person:', error);
            }
          });
      }
    }
  }

  onCancel() {
    this.cancel.emit();
  }

  createPerson(person: PersonUpsertViewModel) {
    this.personService.create(person);
  }

  updatePerson(id: number, person: PersonUpsertViewModel) {
    this.personService.update(id, person);
  }
}
