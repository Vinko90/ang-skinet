# Skinet
Demo shopping app with Angular, NET6 &amp; Stripe.

## Stripe
For Stripe, the Web project make use of secret keys to define public and private keys
for API and Client code.

- In VStudio open the secret manager on the Skinet.Web project and fill the values:

````json
{
  "StripeSettings": {
    "PubKey": "",
    "SecKey": "",
    "WhKey": ""
  }
}
````

- In the ClientApp Angular project create a .env file in the environments folder and fill:

````bash
DEV_STRIPE_PUB_KEY=yourdevkey
PROD_STRIPE_PUB_KEY=yourprodkey
````

Run npm run config to automatically generate the environment files for you.
The config action is also added to the npm build command.
