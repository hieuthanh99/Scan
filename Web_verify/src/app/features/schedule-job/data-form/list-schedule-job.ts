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

export const code = () => new TextboxItem({
  key: 'code',
  label: 'code',
  placeholder: 'code',
  value: '',
  required: true,
});


export const name = () => new TextboxItem({
  key: 'name',
  label: 'name',
  placeholder: 'name',
  value: '',
  required: true,
});

export const value = () => new TextboxItem({
  key: 'value',
  label: 'value',
  placeholder: 'value',
  value: '',
  required: true,
});

export const status = () => new TextboxItem({
  key: 'status',
  label: 'status',
  placeholder: 'status',
  value: '',
  required: true,
});

export const description = () => new TextboxItem({
  key: 'description',
  label: 'description',
  placeholder: 'description',
  value: '',
  required: true,
});

export const action = () => new TextboxItem({
  key: 'action',
  label: 'action',
  placeholder: 'action',
  value: '',
  required: true,
});
