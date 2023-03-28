export interface IRequestResultModel<T> extends IRequestResultModelNoData {
  data: T;
}

export interface IRequestResultModelNoData {
  isSuccessful: boolean;
  message: string;
}
