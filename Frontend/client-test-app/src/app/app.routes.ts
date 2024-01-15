import { RouterModule, Routes } from '@angular/router';
import { UserRegistrationComponent } from './register-user/register-user.component';
import { SelectUserCountryComponent } from './select-user-country/select-user-country.component';

export  const APP_ROUTES: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'register' },
  { path: 'register', component: UserRegistrationComponent },
  { path: 'select-country', component: SelectUserCountryComponent },
];
