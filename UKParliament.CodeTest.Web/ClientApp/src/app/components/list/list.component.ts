import { Component, OnInit } from '@angular/core';
import { PersonService } from '../../services/person.service';
import { DepartmentService } from '../../services/department.service';
import { FormGroup } from '@angular/forms';
import { PersonViewModel } from '../../models/person-view-model';
import { Observable } from 'rxjs';
import { DepartmentViewModel } from '../../models/department-view-model';

@Component({
  selector: 'app-home',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})

export class ListComponent implements OnInit {
  persons: Observable<PersonViewModel[]> | null = null;
  editedPerson: PersonViewModel | null = null;
  departments: Observable<DepartmentViewModel[]> | null = null;

  ngOnInit() {
    this.persons = this.listPersons();
    this.departments = this.departmentService.list();
  }

  edit(person: PersonViewModel) {
    this.editedPerson = person;
  }

  add() {
    this.editedPerson = {
      id: 0,
      firstName: '',
      lastName: '',
      birthDate: '',
      departmentId: 1,
    }
  }

  onUpdate(person: PersonViewModel) {
    this.editedPerson = null;
    this.persons = this.listPersons();
  }

  onCreate(person: PersonViewModel) {
    this.editedPerson = null;
    this.persons = this.listPersons();
  }

  onCancel() {
    this.editedPerson = null;
  }

  constructor(private personService: PersonService, private departmentService: DepartmentService) {
  }

  listPersons(): Observable<PersonViewModel[]> {
    return this.personService.list();
  }

  getPersonById(id: number): Observable<PersonViewModel> {
    return this.personService.getById(id);

    //this.personService.getById(id).subscribe({
    //  next: (result) => console.info(`User returned: ${JSON.stringify(result)}`),
    //  error: (e) => console.error(`Error: ${e}`)
    //});
  }
}
