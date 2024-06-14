import { PlanoContaModel } from "./planoConta";

export interface Transacao {
  codigo: number;
  descricao: string;
  data: Date;
  planoConta: PlanoContaModel;
  valor: number;
}
