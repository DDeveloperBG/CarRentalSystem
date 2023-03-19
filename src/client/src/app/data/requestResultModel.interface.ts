export interface IRequestResultModel<T> {
  data: T;
  isSuccessful: boolean;
  message: string;
}
