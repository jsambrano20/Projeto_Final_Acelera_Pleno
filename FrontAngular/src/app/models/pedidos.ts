import { Mesa } from "./mesa";
import { Prato } from "./prato";

export interface Pedidos {
  id: string;
  cpf:string;
  quantidade: number;
  statuspedido: statusPedido;
  dtRecebimento: Date;
  pratos:Prato;
  mesas: Mesa;
}

enum statusPedido
{
  Recebido = 1,
  Preparando = 2,
  Disponivel = 3,
  Entregue = 4,
  Cancelado = 5,
  Baixado = 6,
}
