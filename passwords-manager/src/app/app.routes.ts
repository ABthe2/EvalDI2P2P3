import { Routes } from '@angular/router';
import {PasswordListComponent} from "./password-list/password-list.component";
import {ApplicationListComponent} from "./application-list/application-list.component";
import {AddPasswordComponent} from "./add-password/add-password.component";
import {HomeComponent} from "./home/home.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'passwords', component: PasswordListComponent },
  { path: 'applications', component: ApplicationListComponent },
  { path: 'passwords/add', component: AddPasswordComponent },
];
