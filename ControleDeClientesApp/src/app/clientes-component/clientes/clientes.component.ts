import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Cliente } from 'src/models/cliente';
import { ClienteService } from 'src/services/cliente.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.scss']
})
export class ClientesComponent implements OnInit {
  clientes: Observable<Cliente[]> = of([]);

  constructor(private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.loadClientes();
  }

  loadClientes() {
    this.clientes = this.clienteService.getClientes();
  }

  delete(clienteId: number = 0) {
    const ans = confirm('Deseja realmente excluir o cliente: ' + clienteId);
    if (ans) {
      this.clienteService.deleteCliente(clienteId).subscribe((data) => {
        this.loadClientes();
      });
    }
  }
}
