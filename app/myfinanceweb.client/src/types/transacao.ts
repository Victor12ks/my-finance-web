import { PlanoContaModel } from "./planoConta";

export interface TransacaoModel {
  codigo?: number;
  historico: string;
  dataHora: Date;
  planoContaId: number;
  planoConta: PlanoContaModel;
  valor: number;
}
