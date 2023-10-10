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
    label: 'Mã thao tác',
    placeholder: 'Mã thao tác',
    value: '',
    required: false,
});
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên thao tác',
    placeholder: 'Tên thao tác',
    value: '',
    required: false,
});