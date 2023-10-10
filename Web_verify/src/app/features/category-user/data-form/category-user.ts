import {
    TextboxItem
} from '@shared-sm';
export const username = () => new TextboxItem({
    key: 'username',
    label: 'Tên đăng nhập',
    placeholder: 'Tên đăng nhập',
    value: '',
    required: false,
});