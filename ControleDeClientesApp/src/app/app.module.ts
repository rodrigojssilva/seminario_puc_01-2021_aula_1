import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClientesComponent } from './clientes-component/clientes/clientes.component';
import { ClienteAddEditComponent } from './clientes-component/cliente-add-edit/cliente-add-edit.component';
import { ClienteViewComponent } from './clientes-component/cliente-view/cliente-view.component';
import { ClienteService } from 'src/services/cliente.service';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    ClientesComponent,
    ClienteAddEditComponent,
    ClienteViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    ClienteService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
