for generating bin file
java -jar bundlesigner-0.1.13.jar genbin  --bundle in.ardin.playkon.aab --bin . --v2-signing-enabled true --v3-signing-enabled true --ks playkon.keystore -v

for generating key file
keytool -genkey -v -keystore playkon.keystore -alias com.playkon -keyalg RSA -keysize 2048 -validity 10000

