import {
    TextboxItem
} from '@shared-sm';

export const templateId = () => new TextboxItem({
    key: 'templateId',
    label: 'Template Id',
    placeholder: 'Template Id',
    value: '',
    required: false,
});

export const subject = () => new TextboxItem({
    key: 'subject',
    label: 'Tiêu đề',
    placeholder: 'Tiêu đề',
    value: '',
    required: false,
});