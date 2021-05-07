import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClienteAddEditComponent } from './clientes-component/cliente-add-edit/cliente-add-edit.component';
import { ClienteViewComponent } from './clientes-component/cliente-view/cliente-view.component';
import { ClientesComponent } from './clientes-component/clientes/clientes.component';

const routes: Routes = [
  { path: '', component: ClientesComponent, pathMatch: 'full' }, //este componente é a página inicial
  
  { path: 'cliente/view/:id', component: ClienteViewComponent },
  { path: 'cliente/add', component: ClienteAddEditComponent },
  { path: 'cliente/edit/:id', component: ClienteAddEditComponent },
  
  { path: '**', redirectTo: '/' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
