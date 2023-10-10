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
    key: 'appName',
    label: 'Tên ứng dụng',
    placeholder: 'Tên ứng dụng',
    value: '',
    required: false,
});

export const status = () => new DropdownItem({
    key: 'status',
    label: 'Trạng thái',
    value: '',
    options: [
        {key: '', value: 'Tất cả', checked: true},
        {key: 'true', value: 'Active'},
        {key: 'false', value: 'Deactive'},
    ]
});