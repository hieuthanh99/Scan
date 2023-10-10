import {
    TextboxItem
} from '@shared-sm';
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên Resource',
    placeholder: 'Tên Resource',
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