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
export const key = () => new TextboxItem({
    key: 'key',
    label: 'Key',
    placeholder: 'Key',
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