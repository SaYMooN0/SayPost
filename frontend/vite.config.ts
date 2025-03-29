import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
	plugins: [sveltekit()],
	server: {
        proxy: {
            '^/api/auth': {
                target: 'https://localhost:5177/',
                secure: false,
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, '')
            },
			'^/api/main': {
                target: 'https://localhost:5124/',
                secure: false,
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, '')
            },	
			'^/api/notifications': {
                target: 'https://localhost:5026/',
                secure: false,
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, '')
            },

        },
        port: 5173
		// ,https: {
        //     key: fs.readFileSync(keyFilePath),
        //     cert: fs.readFileSync(certFilePath),
        // }
    }
});
