import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './component/login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';
import { MenuComponent } from './component/menu/menu.component';
import { HeaderComponent } from './component/header/header.component';
import { PedidosComponent } from './component/pedidos/pedidos.component';
import { FooterComponent } from './component/footer/footer.component';
import { LoaderComponent } from './loader/loader.component';
import { NgxLoadingModule } from 'ngx-loading';


@NgModule({
  declarations: [AppComponent, LoginComponent, MenuComponent, HeaderComponent, PedidosComponent, FooterComponent, LoaderComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    NgxLoadingModule.forRoot({
      fullScreenBackdrop: true
    })
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule { }
