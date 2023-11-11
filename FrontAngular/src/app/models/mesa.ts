import { Cliente } from 'src/app/models/cliente';
export interface Mesa {
  id: string;
  descricao: string;
  lugares: number;
  clienteId: string;
  clientes: Cliente;
  ocupada: boolean;
  ambiente: string;
  statusMesa: number;
  dataInclusao: Date;
  dataAlteracao: Date;
}
