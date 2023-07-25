package SAR0083.Homework;

import org.springframework.core.convert.converter.Converter;

import SAR0083.Homework.models.EmailAddress;

public class StringToEmailAddressConverter implements Converter<String, EmailAddress> {
	// Methods: override
	@Override
	public EmailAddress convert(String from) {
		try {
			String[] fromParts = from.split("@");
			
			return new EmailAddress(fromParts[0], fromParts[1]);
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return null;
	}
}