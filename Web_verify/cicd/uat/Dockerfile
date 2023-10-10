FROM 10.1.16.211:8005/anhpq2-image-base/node_nexus:16.19.0 AS build
WORKDIR /build
COPY . .
RUN pwd
RUN cd /build/src/app && npm cache clean --force && npm install
RUN cd /build/src/app && npm run build

FROM k8sdev.mbbank.com.vn/support/nginx:1.21.4
COPY --from=build /build/dist/cms_prortal /usr/share/nginx/html
COPY cicd/uat/configmap-k8s/nginx-custom.conf /etc/nginx/conf.d/default.conf
