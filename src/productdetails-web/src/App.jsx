import { ApolloProvider, ApolloClient, InMemoryCache } from "@apollo/client";
import ProductList from "./components/ProductList"

const client = new ApolloClient({
  uri: "http://localhost:5076/graphql/",
  cache: new InMemoryCache()
});

const App = () => {
  return (
    <ApolloProvider client={client}>
      <ProductList />
    </ApolloProvider>
  )
}

export default App