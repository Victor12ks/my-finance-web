export type ResponseBase<TResponse> = {
  success: boolean;
  message: string;
  data?: TResponse;
};
