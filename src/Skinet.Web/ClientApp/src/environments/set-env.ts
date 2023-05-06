/* eslint-disable @typescript-eslint/no-var-requires */
const setEnv = () => {
  const fs = require('fs');
  const writeFile = fs.writeFile;
  // Configure Angular dev and prod `environment.ts` file paths
  const devTargetPath = './src/environments/environment.ts';
  const prodTargetPath = './src/environments/environment.prod.ts';
  //Hidden file where to get keys
  require('dotenv').config({
    path: 'src/environments/.env'
  });

  // ****** Development File ******
  const devEnvConfigFile = `export const environment = {
    production: false,
    stripePubKey: '${process.env.DEV_STRIPE_PUB_KEY}'
  };`;

  console.log('The dev file `environment.ts` will be written with the following content: \n' + devEnvConfigFile);
  writeFile(devTargetPath, devEnvConfigFile, (err: any) => {
    if (err) {
      console.error(err);
      throw err;
    } else {
      console.log(`Angular Dev environment.ts file generated correctly at ${devTargetPath} \n`);
    }
  });


  // ****** Production File ******
  const prodEnvConfigFile = `export const environment = {
    production: true,
    stripePubKey: '${process.env.PROD_STRIPE_PUB_KEY}'
  };`;

  console.log('The production file `environment.prod.ts` will be written with the following content: \n' + prodEnvConfigFile);
  writeFile(prodTargetPath, prodEnvConfigFile, (err: any) => {
    if (err) {
      console.error(err);
      throw err;
    } else {
      console.log(`Angular Production environment.prod.ts file generated correctly at ${prodTargetPath} \n`);
    }
  });
};

setEnv();
