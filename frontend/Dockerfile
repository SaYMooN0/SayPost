FROM node:20.17.0

WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .

ENV NODE_ENV=production

RUN npm run build

EXPOSE 4173

CMD ["npm", "run", "preview"]
