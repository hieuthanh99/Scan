import {
    HiddenItem,
    TextAreaItem,
    TextboxItem
} from '@shared-sm';
export const id = () => new HiddenItem({
    key: 'id',
    label: 'id thao tác',
    value: '',
});
export const code = () => new TextboxItem({
    key: 'code',
    label: 'Mã thao tác',
    placeholder: 'Mã thao tác',
    value: '',
    required: true,
});
export const name = () => new TextboxItem({
    key: 'name',
    label: 'Tên thao tác',
    placeholder: 'Tên thao tác',
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