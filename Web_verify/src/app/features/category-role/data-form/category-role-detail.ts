import {
    HiddenItem,
    TextAreaItem,
    TextboxItem
} from '@shared-sm';
export const id = () => new HiddenItem({
    key: 'id',
    label: 'id vai trò',
    value: '',
});
export const code = () => new TextboxItem({
    key: 'code',
    label: 'Mã vai trò',
    placeholder: 'Mã vai trò',
    value: '',
    required: true,
});
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên vai trò',
    placeholder: 'Tên vai trò',
    value: '',
    required: true,
});
export const description = () => new TextAreaItem({
    key: 'description',
    label: 'Mô tả',
    placeholder: 'Mô tả',
    value: '',
    required: false,
});