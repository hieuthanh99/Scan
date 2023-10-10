import {
  DropdownItem, NgSelectItem,
  TextboxItem
} from '@shared-sm';
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
