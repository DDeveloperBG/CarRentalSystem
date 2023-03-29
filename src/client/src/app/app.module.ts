import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './modules/core/core.module';
import { SharedModule } from './modules/shared/shared.module';
import { UserModule } from './modules/user/user.module';
import { CarModule } from './modules/car/car.module';
import { AdminModule } from './modules/admin/admin.module';

import { storageServiceProvider } from '@services/storage.service';
import { UserService } from '@services/user.service';
import { CarService } from '@services/car.service';
import { PickupLocationService } from '@services/pickupLocation.service';

import { AuthGuard } from '@guards/auth.guard';

import { JwtInterceptor } from '@interceptors/jwtInterceptor';

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';

@NgModule({
  declarations: [AppComponent, HomeComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    UserModule,
    CarModule,
    AdminModule,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers: [
    AuthGuard,
    storageServiceProvider,
    UserService,
    CarService,
    PickupLocationService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
