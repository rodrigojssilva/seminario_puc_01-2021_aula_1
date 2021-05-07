import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Cliente } from 'src/models/cliente';
import { ClienteService } from 'src/services/cliente.service';

@Component({
  selector: 'app-cliente-view',
  templateUrl: './cliente-view.component.html',
  styleUrls: ['./cliente-view.component.scss']
})
export class ClienteViewComponent implements OnInit {
  cliente: Observable<Cliente> = of();
  clienteId: number = 0;
  
  constructor(
    private clienteService: ClienteService,
    private avRoute: ActivatedRoute
  ) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) { //recupera o id passado por parâmetro
      this.clienteId = this.avRoute.snapshot.params[idParam];
    }
   }

  ngOnInit(): void {
    this.loadCliente();
  }

  loadCliente() {
    this.cliente = this.clienteService.getCliente(this.clienteId);
  }
}
