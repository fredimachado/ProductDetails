const ProductPrice = ({ price, wasPrice }) => {
  function getDollarAmount(price) {
    return parseInt(price);
  }
  function getCentAmount(price) {
    return getPrice(price).split(".")[1];
  }
  function getPrice(price) {
    return price.toFixed(2);
  }

  return (
    <h4 className="pb-1 text-2xl font-extrabold">
      ${getDollarAmount(price)}
      <span className="relative -top-2 text-xs">{getCentAmount(price)}</span>
      {wasPrice && <span className="ml-2 text-xs text-gray-500 font-normal">Was ${getPrice(wasPrice)}</span>}
    </h4>
  )
}

export default ProductPrice
