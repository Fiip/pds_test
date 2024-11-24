import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonViewModel } from '../models/person-view-model';
import { PersonUpsertViewModel } from '../models/person-upsert-view-model';

@Injectable({
  providedIn: 'root'
})

export class PersonService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  create(data: PersonUpsertViewModel): Observable<Object> {
    return this.http.post(this.baseUrl + `api/person`, data);
  }

  getById(id: number): Observable<PersonViewModel> {
    return this.http.get<PersonViewModel>(this.baseUrl + `api/person/${id}`)
  }

  list(): Observable<PersonViewModel[]> {
    return this.http.get<PersonViewModel[]>(this.baseUrl + `api/person`)
  }

  update(id: number, data: PersonUpsertViewModel): Observable<Object> {
    return this.http.put(this.baseUrl + `api/person/${id}`, data)
  }
}
