import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Cliente } from 'src/models/cliente';
import { ClienteService } from 'src/services/cliente.service';

@Component({
  selector: 'app-cliente-add-edit',
  templateUrl: './cliente-add-edit.component.html',
  styleUrls: ['./cliente-add-edit.component.scss']
})
export class ClienteAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formNome: string;
  formDocumento: string;
  clienteId: number = 0;
  errorMessage: any;
  existingCliente: Cliente = new Cliente();

  constructor(
    private clienteService: ClienteService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router
  ) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formNome = 'nome';
    this.formDocumento = 'documento';

    if (this.avRoute.snapshot.params[idParam]) {
      this.clienteId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        clienteId: 0,
        nome: ['', [Validators.required]],
        documento: ['', [Validators.required]]
      }
    )
  }

  ngOnInit(): void {
    if (this.clienteId > 0) {
      this.actionType = 'Edit';
      this.clienteService.getCliente(this.clienteId)
        .subscribe(result => (
          this.existingCliente = result,
          this.form.controls[this.formNome].setValue(result.nome),
          this.form.controls[this.formDocumento].setValue(result.documento)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      alert("Campos obrigatórios não preenchidos!");
      return;
    }

    if (this.actionType === 'Add') {
      let cliente: Cliente = {
        clienteId: 0,
        nome: this.form.get(this.formNome)?.value,
        documento: this.form.get(this.formDocumento)?.value
      };

      this.clienteService.saveCliente(cliente)
        .subscribe((result) => {
          alert('Cliente cadastrado com sucesso!');
          this.router.navigate(['/cliente/view', result.clienteId]);
        });
    }

    if (this.actionType === 'Edit') {
      let cliente: Cliente = {
        clienteId: this.existingCliente.clienteId,
        nome: this.form.get(this.formNome)?.value,
        documento: this.form.get(this.formDocumento)?.value
      };

      if (cliente.clienteId != null)
        this.clienteService.updateCliente(cliente.clienteId, cliente)
          .subscribe((data) => {
            alert('Cliente atualizado com sucesso!');
            // this.router.navigate([this.router.url]);

            this.cancel();
          });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  get nome() { return this.form.get(this.formNome); }
  get documento() { return this.form.get(this.formDocumento); }
}
