import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ListComponent } from './components/list/list.component';
import { UpsertComponent } from './components/upsert/upsert.component';

@NgModule({ declarations: [
  AppComponent,
  ListComponent,
  UpsertComponent
    ],
    bootstrap: [AppComponent], imports: [BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', component: ListComponent, pathMatch: 'full' }
        ])], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class AppModule { }
