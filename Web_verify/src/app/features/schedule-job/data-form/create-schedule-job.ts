import {
    CheckboxItem,
    DateTimeItem,
    DropdownItem,
    NgSelectItem,
    RadioItem,
    SlideItem,
    STATUS,
    TextAreaItem,
    TextboxItem
} from '@shared-sm';
import * as moment from "moment"

//   'code',
//   'name',
//   'status',
//   'description',
//   'action'



export const code = () => new TextboxItem({
  key: 'code',
  label: 'Mã',
  placeholder: '',
  value: '',
  required: true,
});

export const processId = () => new TextboxItem({
  key: 'processId',
  label: 'ID',
  placeholder: '',
  value: '',
  required: true,
});


export const name = () => new TextboxItem({
  key: 'name',
  label: 'Tên',
  placeholder: '',
  value: '',
  required: true,
});

export const expression = () => new TextboxItem({
  key: 'expression',
  label: 'Biểu thức tiến trình',
  placeholder: '* * * * *',
  value: '',
  readOnly: true,
  required: true,
});

export const status = () => new TextboxItem({
  key: 'status',
  label: 'Trạng thái',
  placeholder: '',
  value: 'true',
  required: true,
});

export const description = () => new TextboxItem({
  key: 'description',
  label: 'Mô tả',
  placeholder: '',
  value: '',
  required: true,
});

export const className = () => new TextboxItem({
  key: 'className',
  label: 'Lớp tiến trinh',
  placeholder: 'VD: vn.com.mbbank.job.ExampleJobV1',
  value: '',
  required: true,
});

export const startTime = () => new DateTimeItem({
  key: 'startTime',
  label: 'Thời gian bắt đầu',
  placeholder: '',
  value: '',
  minDate: moment(new Date().setDate(new Date().getDate())).format('YYYY-MM-DD HH:mm:ss'),
  maxDate: moment(new Date().setDate(new Date().getDate() + 365)).format('YYYY-MM-DD HH:mm:ss'),
  required: true,
});
