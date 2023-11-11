import { Mesa } from 'src/app/models/mesa';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { Prato } from 'src/app/models/prato';
import { Pedidos } from 'src/app/models/pedidos';
import { PratoService } from 'src/app/services/prato/prato.service';
import { TokenService } from 'src/app/services/token.service';
import { PedidoService } from 'src/app/services/pedido/pedido.service';
import { RealizarPedido } from 'src/app/models/realizar-pedido';
import { TransporteServiceService } from './../../services/transporte/transporte-service.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
})
export class MenuComponent {
  token: string;
  pratos: Prato[];
  pedidos: Pedidos[];
  pedido: Pedidos;
  mesas: Mesa[];
  idMesa: string = '013ed28f-d7cf-4afc-976c-3196851ad772';
  cpf: string = '11111111111';
  quantidade: number;
  //@ViewChild('quantidadeInput', {static: false}) quantidadeInput: ElementRef;

  realizarPedido: RealizarPedido = {
    mesaid: '',
    pratoid: '',
    cpf: '',
    quantidade: 0,
  };

  constructor(
    private tokenService: TokenService,
    private pratoService: PratoService,
    private pedidosService: PedidoService,
    private transporte: TransporteServiceService
  ) {}

  ngOnInit() {
    this.GetPratos();
  }

  TrazerDados() {
    this.transporte.getObjeto().subscribe((obj) => {
      this.mesas = obj;
      console.log(this.mesas);
    });
  }

  RealizarPedido(id: string) {
    console.log(Number(this.pratos.find((a) => a.id === id)?.quantidade));
    this.quantidade=Number(this.pratos.find((a) => a.id === id)?.quantidade)
    this.TrazerDados();
    setTimeout(() => {
      console.log(this.mesas[0].clientes.cpf);
      this.realizarPedido.cpf = this.mesas[0].clientes.cpf.toString();
      this.realizarPedido.mesaid = this.mesas[0].id.toString();
      this.realizarPedido.pratoid = id;
      this.realizarPedido.quantidade = this.quantidade;
      this.postPedido(this.realizarPedido);
    }, 1000);
  }

  postPedido(pedido: RealizarPedido) {
    this.tokenService.getToken().subscribe((tokenUser) => {
      this.token = tokenUser.token;
      this.pedidosService
        .postPedido(pedido, this.token)
        .subscribe((pedidos: Pedidos) => {
          this.pedido = pedidos;
          console.log(this.pedido);
        });
    });
  }

  GetPedidosCPfMesa() {
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

  GetPratos() {
    this.tokenService.getToken().subscribe((tokenUser) => {
      this.token = tokenUser.token;
      this.pratoService.getPratos(this.token).subscribe((pratos: Prato[]) => {
        this.pratos = pratos.filter((prato) => prato.status);
        console.log(this.pratos);
      });
    });
  }
}
