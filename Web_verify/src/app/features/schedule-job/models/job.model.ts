export type IJob = {
  processId?:        number;
  code?:             string;
  name?:             string;
  status?:           number;
  description?:      string;
  createdDate?:      Date;
  updatedDate?:      Date;
  appCode?:          string;
  processScheduler?: ProcessScheduler;
}

export type ProcessScheduler = {
  processSchedulerId?: number;
  startTime?:          Date;
  expression?:         string;
}
