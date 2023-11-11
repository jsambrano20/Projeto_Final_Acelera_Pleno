import { Component } from '@angular/core';
import { Pedidos } from 'src/app/models/pedidos';
import { RealizarPedido } from 'src/app/models/realizar-pedido';
import { PedidoService } from 'src/app/services/pedido/pedido.service';
import { TokenService } from 'src/app/services/token.service';
import { TransporteServiceService } from './../../services/transporte/transporte-service.service';
import { Mesa } from 'src/app/models/mesa';

@Component({
  selector: 'app-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.css'],
})
export class PedidosComponent {
  token: string;
  pedidos: Pedidos[];
  pedido: Pedidos;
  mesas: Mesa[];
  idMesa: string = '';
  cpf: string = '';



  constructor(
    private tokenService: TokenService,
    private pedidosService: PedidoService,
    private transporte: TransporteServiceService
  ) {}

  ngOnInit() {
    this.TrazerDados();

  }

  TrazerDados() {

    this.transporte.getObjeto().subscribe((obj) => {
      this.mesas = obj;
      this.cpf = this.mesas[0].clientes.cpf;
      this.idMesa = this.mesas[0].id;
      console.log(this.mesas);
      this.GetPedidosCPfMesa();
    });
    setTimeout(()=>{
      this.GetPedidosCPfMesa();
    },800)

  }

  Cancelar(id: string) {
    this.tokenService.getToken().subscribe((tokenUser) => {
      this.token = tokenUser.token;
      debugger
      this.pedidosService
        .cancelarPedido(id,tokenUser.token)
        .subscribe((pedidos: Pedidos) => {
          this.pedido = pedidos;
          console.log(this.pedidos);
          this.GetPedidosCPfMesa()
        });
    });
  }

  GetPedidosCPfMesa() {
    console.log('Tazendo pedidos')
    this.tokenService.getToken().subscribe((tokenUser) => {
      this.token = tokenUser.token;
      this.pedidosService
        .getPedidosByMesaCPF(this.cpf, this.idMesa, this.token)
        .subscribe((pedidos: Pedidos[]) => {
          this.pedidos = pedidos;
          console.log(this.pedidos);
        });
    });
  }
}
