import axios, { AxiosResponse } from "axios";
import { ResponseBase } from "../types/reponse";
import { DashboardModel } from "../types/dashboard";

const API_URL = "https://localhost:7293/Dashboard/v1";

export const getDashboard = async (
  dataInicio: string,
  dataFim: string
): Promise<ResponseBase<DashboardModel>> => {
  try {
    const response: AxiosResponse<ResponseBase<DashboardModel>> =
      await axios.get(API_URL, {
        params: {
          dataInicio: dataInicio,
          dataFim: dataFim,
        },
      });
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
