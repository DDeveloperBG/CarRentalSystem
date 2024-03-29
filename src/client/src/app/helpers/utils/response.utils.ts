import {
  IRequestResultModel,
  IRequestResultModelNoData,
} from '@data/requestResultModel.interface';

export function extractResponse<T>(response: any): IRequestResultModel<T> {
  const result: IRequestResultModel<T> = {
    data: response?.data,
    isSuccessful: response?.isSuccessful,
    message: response?.message,
  };
  return result;
}

export function extractResponseNoData(
  response: any
): IRequestResultModelNoData {
  const result: IRequestResultModelNoData = {
    isSuccessful: response?.isSuccessful,
    message: response?.message,
  };
  return result;
}
