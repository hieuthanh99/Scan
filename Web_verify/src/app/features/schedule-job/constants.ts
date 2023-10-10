export const PATH = {
  LIST: 'portal-integration/process/find-all',
  UPDATE: 'portal-integration/process/save',
  CREATE: 'portal-integration/process/save',
  SEARCH: 'portal-integration/process/find-all',
  HISTORY: 'portal-integration/process/find-all-histories',
  DELETE: 'portal-integration/process',
  START_JOB: 'portal-integration/process/start-process',
  STOP_JOB: 'portal-integration/process/stop-process',
};
export const ROLE_MAKER = 'ROLE_MAKER';
export const ROLE_APPROVER = 'ROLE_APPROVER';

export const JOB_STATUS = [
  {
    key: 2,
    value: 'Đang chạy',
    class: 'wf-status-approved'
  },
  {
    key: 1,
    value: 'Tạm ngừng',
    class: 'wf-status-reject'
  }
];
