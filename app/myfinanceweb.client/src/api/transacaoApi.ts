import axios, { AxiosResponse } from "axios";
import { TransacaoModel } from "../types/transacao";
import { ResponseBase } from "../types/reponse";

const API_URL = "https://localhost:7293/Transacao/v1";

export const createTransacao = async (
  planoConta: TransacaoModel
): Promise<ResponseBase<TransacaoModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<TransacaoModel>> =
      await axios.post(API_URL, planoConta);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.message);
      throw new Error(error.message);
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred");
    }
  }
};

export const getTransacoes = async (): Promise<
  ResponseBase<TransacaoModel[]>
> => {
  try {
    const response: AxiosResponse<ResponseBase<TransacaoModel[]>> =
      await axios.get(API_URL);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.message);
      throw new Error(error.message);
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred");
    }
  }
};

export const updateTransacao = async (
  planoConta: TransacaoModel
): Promise<ResponseBase<TransacaoModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<TransacaoModel>> =
      await axios.put(API_URL, planoConta);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.message);
      throw new Error(error.message);
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred");
    }
  }
};

export const removeTransacao = async (
  planoConta: TransacaoModel
): Promise<ResponseBase<TransacaoModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<TransacaoModel>> =
      await axios.delete(`${API_URL}/${planoConta.codigo}`);
    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.error("Axios error:", error.message);
      throw new Error(error.message);
    } else {
      console.error("Unexpected error:", error);
      throw new Error("Unexpected error occurred");
    }
  }
};
