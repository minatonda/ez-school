import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AuthComponent } from './component/auth/auth.component';
import { LayoutNavbarComponent } from './component/layout-navbar/layout-navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    LayoutNavbarComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent, LayoutNavbarComponent]
})
export class AppModule { }
