import {
    TextboxItem
} from '@shared-sm';
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên hệ thống',
    placeholder: 'Tên hệ thống',
    value: '',
    required: true,
});
export const key = () => new TextboxItem({
    key: 'key',
    label: 'Key',
    placeholder: 'Key',
    value: '',
    required: true,
});