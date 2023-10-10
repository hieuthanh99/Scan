export interface IErrorModel {
  title?: string;
  errorCode?: string;
  message?: string;
  innerHTML?: any;
  closeBtn?: string;
  panelClass?: string; /* for custom style */
  hidenBtn?: boolean;
}

export interface IConfirmModel {
  title?: string;
  message?: string;
  innerHTML?: any;
  acceptBtn?: string;
  closeBtn?: string;
  panelClass?: string; /* for custom style */
  description?: string;
  confirm?: string;
  position?: any;
  data?: any;
  note?: string;
  isShowCheckBox?: boolean;
}
