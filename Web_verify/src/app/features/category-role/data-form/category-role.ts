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
    label: 'Mã vai trò',
    placeholder: 'Mã vai trò',
    value: '',
    required: false,
});
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên vai trò',
    placeholder: 'Tên vai trò',
    value: '',
    required: false,
});