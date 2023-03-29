import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { RouterModule } from '@angular/router';

import { HeaderComponent } from './header/header.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [HeaderComponent, NotFoundComponent],
  exports: [HeaderComponent, NotFoundComponent],
  imports: [CommonModule, SharedModule, RouterModule],
})
export class CoreModule {}
