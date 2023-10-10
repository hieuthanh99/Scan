import {
    TextboxItem
} from '@shared-sm';

export const templateId = () => new TextboxItem({
    key: 'id',
    label: 'Template Id',
    placeholder: 'Template Id',
    value: '',
    required: true,
    maxLength: 100,
});

export const subject = () => new TextboxItem({
    key: 'subject',
    label: 'Tiêu đề',
    placeholder: 'Tiêu đề',
    value: '',
    required: true,
    maxLength: 4000,
});

export const body = () => new TextboxItem({
    key: 'body',
    label: 'Nội dung',
    placeholder: 'Nội dung',
    value: '',
    required: true,
    maxLength: 4000,
    minRow: "5"
});

export const description = () => new TextboxItem({
    key: 'description',
    label: 'Mô tả',
    placeholder: 'Mô tả',
    value: '',
    required: true,
    maxLength: 4000,
});


