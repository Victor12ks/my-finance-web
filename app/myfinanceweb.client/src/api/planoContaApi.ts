import axios, { AxiosResponse } from "axios";
import { PlanoContaModel } from "../types/planoConta";
import { ResponseBase } from "../types/reponse";

const API_URL = "https://localhost:7293/PlanoConta/v1";

export const createPlanoConta = async (
  planoConta: PlanoContaModel
): Promise<ResponseBase<PlanoContaModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<PlanoContaModel>> =
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

export const getPlanosConta = async (): Promise<
  ResponseBase<PlanoContaModel[]>
> => {
  try {
    const response: AxiosResponse<ResponseBase<PlanoContaModel[]>> =
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

export const updatePlanoConta = async (
  planoConta: PlanoContaModel
): Promise<ResponseBase<PlanoContaModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<PlanoContaModel>> =
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

export const disableEnablePlanoConta = async (
  planoConta: PlanoContaModel
): Promise<ResponseBase<PlanoContaModel>> => {
  const action = "disable-enable";
  try {
    const response: AxiosResponse<ResponseBase<PlanoContaModel>> =
      await axios.put(`${API_URL}/${action}`, planoConta);
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
