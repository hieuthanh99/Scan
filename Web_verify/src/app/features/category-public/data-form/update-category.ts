import {
  SlideItem,
  TextboxItem
} from '@shared-sm';
export const code = () => new TextboxItem({
  key: 'code',
  label: 'Mã',
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

export const value = () => new TextboxItem({
  key: 'value',
  label: 'Giá trị',
  placeholder: '',
  value: '',
  required: true,
});

export const commonCategoryCode = () => new TextboxItem({
  key: 'commonCategoryCode',
  label: 'Mã danh mục',
  placeholder: '',
  value: '',
  required: true,
  readOnly: true
});


export const isDefault = () => new SlideItem({
  key: 'isDefault',
  label: 'Mặc định',
  placeholder: '',
  value: true,
  required: true,
});

export const description = () => new TextboxItem({
  key: 'description',
  label: 'Mô tả',
  placeholder: '',
  value: '',
  required: true,
});

export const orderNum = () => new TextboxItem({
  key: 'orderNum',
  label: 'Thứ tự',
  placeholder: '',
  value: '',
  required: true,
  type: 'number'
});
