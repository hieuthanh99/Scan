import {
    DropdownItem,
    SlideItem,
    TextboxItem
} from '@shared-sm';

export const appCode = () => new TextboxItem({
    key: 'appCode',
    label: 'Mã ứng dụng',
    placeholder: 'Mã ứng dụng',
    value: '',
    required: true,
    maxLength: 255
});

export const appName = () => new TextboxItem({
    key: 'appName',
    label: 'Tên ứng dụng',
    placeholder: 'Tên ứng dụng',
    value: '',
    required: true,
});

export const isKeycloak = () => new SlideItem({
    key: 'isKeycloak',
    placeholder: 'Sử dụng Keycloak',
    value: true
});

export const secretKey = () => new TextboxItem({
    key: 'secretKey',
    label: 'Secret Key',
    placeholder: 'Secret Key',
    value: '',
    required: false,
});

export const status = () => new SlideItem({
    key: 'status',
    placeholder: 'Active',
    value: true
});

export const clientId = () => new TextboxItem({
    key: 'clientId',
    label: 'Keycloak clientId',
    placeholder: 'Keycloak clientId',
    value: '',
    required: false,
    maxLength: 255
});

export const isShowMenu = () => new SlideItem({
    key: 'isShowMenu',
    placeholder: 'Hiển thị trên menu',
    value: true
});

export const type = () => new DropdownItem({
    key: 'type',
    label: 'Tích hợp',
    value: 'micro',
    options: [
        {key: 'micro', value: 'Micro-Frontend', checked: true},
        {key: 'iframe', value: 'Iframe'},
    ]
});

export const subDomain = () => new TextboxItem({
    key: 'subDomain',
    label: 'Subdomain',
    placeholder: 'Subdomain',
    value: '',
    required: false,
});

export const elementName = () => new TextboxItem({
    key: 'elementName',
    label: 'Element name',
    placeholder: 'Element name',
    value: '',
    required: false,
});

export const description = () => new TextboxItem({
    key: 'description',
    label: 'Mô tả',
    placeholder: 'Mô tả',
    value: '',
    required: false,
});