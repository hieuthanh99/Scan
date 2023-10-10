import {
  DropdownItem,
  NgSelectItem,
  TextboxItem
} from '@shared-sm';
export const key = () => new TextboxItem({
    key: 'key',
    label: 'Key',
    placeholder: 'key',
    value: '',
    required: true,
});
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Name',
    placeholder: 'Name',
    value: '',
    required: true,
});

export const search = () => new TextboxItem({
  key: 'search',
  label: '',
  placeholder: 'Nhập từ khóa...',
  value: '',
  required: false,
});

export const fields = () =>
  new DropdownItem({
    key: 'fields',
    placeholder: 'Chọn',
    label: '',
    value: 'code',
    options: [
      {
        key: 'code',
        value: 'Mã'
      },
      {
        key: 'name',
        value: 'Tên'
      }
    ],
  });
