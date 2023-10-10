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
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên Resource',
    placeholder: 'Tên Resource',
    value: '',
    required: false,
});