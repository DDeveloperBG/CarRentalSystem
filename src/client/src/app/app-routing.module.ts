import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './modules/core/not-found/not-found.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: HomeComponent,
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

export const AppRoutingModule = RouterModule.forRoot(routes);
